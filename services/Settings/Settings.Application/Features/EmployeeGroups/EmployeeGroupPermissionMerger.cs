using System.Text.Json.Nodes;

namespace Settings.Application.Features.EmployeeGroups;

/// <summary>Merges several employee-group <c>PermissionsJson</c> blobs (union: booleans OR, strings pick most permissive) and flattens to JWT claim values.</summary>
public static class EmployeeGroupPermissionMerger
{
    public static IReadOnlyList<string> MergeAndFlatten(IReadOnlyList<string> permissionsJsonBlobs)
    {
        JsonNode? merged = null;
        foreach (var json in permissionsJsonBlobs)
        {
            if (string.IsNullOrWhiteSpace(json)) continue;
            try
            {
                var node = JsonNode.Parse(json);
                if (node is null) continue;
                merged = Merge(merged, node);
            }
            catch (System.Text.Json.JsonException)
            {
                /* skip invalid fragment */
            }
        }

        if (merged is null) return [];

        var claims = new List<string>();
        Flatten("", merged, claims);
        return claims;
    }

    private static JsonNode? Merge(JsonNode? a, JsonNode? b)
    {
        if (b is null) return a;
        if (a is null) return b;

        if (a is JsonObject ao && b is JsonObject bo)
        {
            var result = new JsonObject();
            foreach (var p in ao)
                result[p.Key] = p.Value is null ? null : p.Value.DeepClone();
            foreach (var p in bo)
            {
                if (!result.TryGetPropertyValue(p.Key, out var existing) || existing is null)
                    result[p.Key] = p.Value is null ? null : p.Value.DeepClone();
                else
                    result[p.Key] = Merge(existing, p.Value);
            }

            return result;
        }

        if (a is JsonValue va && b is JsonValue vb)
        {
            if (va.TryGetValue<bool>(out var ab) && vb.TryGetValue<bool>(out var bb))
                return ab || bb;

            if (va.TryGetValue<string>(out var sa) && vb.TryGetValue<string>(out var sb))
                return MorePermissiveAccess(sa, sb);

            return vb.DeepClone();
        }

        return b.DeepClone();
    }

    /// <summary>ERP &quot;Access to&quot; dropdown ranks: none &lt; own &lt; ownDepartment &lt; all.</summary>
    private static string MorePermissiveAccess(string a, string b)
    {
        static int Score(string v)
        {
            var x = (v ?? "").Trim().ToLowerInvariant();
            return x switch
            {
                "" => 0,
                "none" => 1,
                "own" => 2,
                "owndepartment" => 3,
                "all" => 4,
                _ => 1
            };
        }

        return Score(a) >= Score(b) ? a : b;
    }

    private static void Flatten(string prefix, JsonNode? node, List<string> claims)
    {
        switch (node)
        {
            case JsonObject o:
                foreach (var p in o)
                {
                    var next = string.IsNullOrEmpty(prefix) ? p.Key : prefix + "." + p.Key;
                    Flatten(next, p.Value, claims);
                }

                break;
            case JsonValue v:
                if (v.TryGetValue<bool>(out var b) && b)
                    claims.Add(prefix);
                else if (v.TryGetValue<string>(out var s) && !string.IsNullOrWhiteSpace(s))
                    claims.Add(prefix + "=" + s.Trim());
                break;
        }
    }
}
