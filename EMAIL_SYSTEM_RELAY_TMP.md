# Email settings & Identity relay — temp notes for PR / later

> **This file is a scratchpad.** Delete or merge into official docs (`services/Settings/README.md`, root `README.md`) when the PR is merged.

## What we built

1. **Settings service — mailbox catalog**
   - Entity `EmailAccountSetting` (SMTP host/port/security, optional Identity `User.Id`, `IsSystemEmail`, encrypted password via ASP.NET Data Protection).
   - CRUD + test-send: `GET|POST|PUT|DELETE /api/email-settings`, `POST /api/email-settings/{id}/test`.
   - UI: `frontend/email-settings.html` (BFF `/api/email-settings` → Settings).

2. **Server-to-server outbound mail**
   - `POST /api/email-settings/system/send` — sends using the **first** row with `IsSystemEmail == true` (oldest by `CreatedAtUtc`).
   - Secured by shared header **`X-AzShipping-System-Email-Key`** matching Settings config **`EmailSystemSend:ApiKey`** (not JWT — for Identity background jobs).

3. **Identity — uses Settings for transactional email (optional)**
   - When **`Settings:UseSystemEmailMailbox`** is `true` and **`Settings:SystemEmailSendApiKey`** matches Settings, **`IEmailService.SendAsync`** calls Settings relay first.
   - If relay fails → logs warning → **fallback** to existing **`Email:`** SMTP block (same as before).
   - Affects: user registration confirmation, forgot password, anything using `SendAsync`.

## Why relay instead of “decrypt password in Identity”

Passwords at rest in Settings are protected with **Data Protection** on the Settings host. Identity cannot decrypt them. Only Settings decrypts and talks to SMTP.

## Config cheat sheet

| Where | Key | Purpose |
|--------|-----|--------|
| Settings | `EmailSystemSend:ApiKey` | Validates `system/send` calls |
| Identity | `Settings:BaseUrl` | e.g. `http://localhost:5064` |
| Identity | `Settings:UseSystemEmailMailbox` | `true` to prefer relay |
| Identity | `Settings:SystemEmailSendApiKey` | **Same string** as Settings `EmailSystemSend:ApiKey` |

Dev defaults (Development JSON): example key `local-dev-email-relay-key-change-me` — **replace in production** with a long random secret on both sides.

## Operational checklist (after PR)

1. Postgres + Settings migrations applied (`EmailAccountSettings` table).
2. At least **one** email row in Settings with **Is system email** checked and **working SMTP** (test from UI or `.../test`).
3. `EmailSystemSend:ApiKey` set on Settings; same value as `SystemEmailSendApiKey` on Identity.
4. **Network:** don't expose `POST .../system/send` to the public internet without extra controls (VPN, internal URL, API gateway).

## Gmail / real SMTP

- Row must use a valid **app password** (or provider equivalent); **Without password** is not enough for Gmail.
- Identity **`Email:Password`** in user secrets is still useful for **fallback** if Settings is down.

## Useful endpoints

```http
# List / CRUD (JWT as usual for Settings)
GET    /api/email-settings
POST   /api/email-settings
GET    /api/email-settings/{id}
PUT    /api/email-settings/{id}
DELETE /api/email-settings/{id}
POST   /api/email-settings/{id}/test

# Internal relay (API key header, AllowAnonymous on this action only)
POST   /api/email-settings/system/send
Header: X-AzShipping-System-Email-Key: <same as EmailSystemSend:ApiKey>
Body:   { "to", "subject", "body", "isHtml": true }
```

## PR description bullets (copy-paste)

- Add Settings **email account** storage (SMTP, Identity user link, system flag) with **encrypted** passwords.
- Add **system send** API for microservices (Identity) so SMTP secrets stay in Settings.
- Identity **opt-in** relay for `IEmailService` with **fallback** to legacy `Email:` configuration.
- Frontend **email-settings** page + BFF route; design-time `SettingsDbContextFactory` for EF migrations.

## One-off dev setup (already done once via API)

- Log in Identity as seeded `admin` / `admin`, JWT to `POST /api/email-settings` with `isSystemEmail: true`.
- Or use `email-settings.html` after login.

---

*Last intent: clarify behavior for reviewers and future you — trim or relocate when no longer needed.*
