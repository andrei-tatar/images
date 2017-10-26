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
    static $inject = ['translationService'];

    languages: string[];
    get selectedLanguage() {
        return this.translationService.selectedLanguage;
    }
    set selectedLanguage(value: string) {
        this.translationService.selectedLanguage = value;
    }

    constructor(
        private translationService: TranslationService
    ) {
        this.languages = translationService.languages        
    }
} 