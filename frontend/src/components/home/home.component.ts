import { AuthService } from './../../services/auth.service';
import { TranslationService } from './../../services/translations.service';
import { Component, Route } from '../../util';

@Component('home', {
    template: require('./home.template.html'),
})
@Route({
    name: 'home',
    url: '',
})
class HomeController {
    static $inject = ['translationService', 'authService'];

    languages: string[];
    get selectedLanguage() {
        return this.translationService.selectedLanguage;
    }
    set selectedLanguage(value: string) {
        this.translationService.selectedLanguage = value;
    }
    get isLoggedIn() {
        return this.authService.isLoggedInFlag;
    }

    constructor(
        private translationService: TranslationService,
        private authService: AuthService,
    ) {
        this.languages = translationService.languages
    }

    logout() {
        this.authService.logout();
    }
} 