// Language Management
const LanguageManager = {
    currentLanguage: 'EN',
    supportedLanguages: {
        'EN': { code: 'EN', name: 'English', flag: '🇬🇧' },
        'LT': { code: 'LT', name: 'Lietuvių', flag: '🇱🇹' },
        'RU': { code: 'RU', name: 'Русский', flag: '🇷🇺' },
        'FR': { code: 'FR', name: 'Français', flag: '🇫🇷' },
        'UK': { code: 'UK', name: 'Українська', flag: '🇺🇦' },
        'PL': { code: 'PL', name: 'Polski', flag: '🇵🇱' },
        'KA': { code: 'KA', name: 'ქართული', flag: '🇬🇪' },
        'AZ': { code: 'AZ', name: 'Azərbaycan', flag: '🇦🇿' }
    },

    init() {
        // Load saved language from localStorage
        const saved = localStorage.getItem('selectedLanguage');
        if (saved && this.supportedLanguages[saved]) {
            this.currentLanguage = saved;
        }
        this.updateLanguageSelector();
        this.applyLanguage();
    },

    setLanguage(languageCode) {
        if (this.supportedLanguages[languageCode]) {
            this.currentLanguage = languageCode;
            localStorage.setItem('selectedLanguage', languageCode);
            this.updateLanguageSelector();
            this.applyLanguage();
            // Trigger custom event for pages to reload data
            window.dispatchEvent(new CustomEvent('languageChanged', { detail: languageCode }));
        }
    },

    getLanguage() {
        return this.currentLanguage;
    },

    getLanguageHeader() {
        return { 'Accept-Language': this.currentLanguage };
    },

    updateLanguageSelector() {
        const selector = document.getElementById('languageSelector');
        if (selector) {
            selector.value = this.currentLanguage;
        }
    },

    applyLanguage() {
        // Update page language attribute
        document.documentElement.lang = this.currentLanguage.toLowerCase();
        
        // Update any language-dependent UI elements
        const event = new CustomEvent('languageApplied', { detail: this.currentLanguage });
        window.dispatchEvent(event);
    },

    createLanguageSelector() {
        const selector = document.createElement('select');
        selector.id = 'languageSelector';
        selector.className = 'language-selector';
        selector.innerHTML = Object.values(this.supportedLanguages)
            .map(lang => `<option value="${lang.code}">${lang.flag} ${lang.name}</option>`)
            .join('');
        selector.value = this.currentLanguage;
        selector.addEventListener('change', (e) => {
            this.setLanguage(e.target.value);
        });
        return selector;
    }
};

// Initialize on page load
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => LanguageManager.init());
} else {
    LanguageManager.init();
}

