import { labels } from '../labels';
import { Service } from '../util';
import * as moment from 'moment';
import * as locales from 'moment/min/locales';

@Service('translationService')
export class TranslationService {
    private _language = 'en';
    private readonly _storageKey = 'language';

    constructor() {
        this.selectedLanguage = localStorage.getItem(this._storageKey);
    }

    get languages() {
        return Object.getOwnPropertyNames(labels).filter(l => l !== '_');
    }

    get selectedLanguage() {
        return this._language;
    }
    set selectedLanguage(value: string) {
        if (labels[value] && this._language !== value) {
            this._language = value;
            localStorage.setItem(this._storageKey, value);
            moment.locale(labels[this._language].locale);
        }
    }

    translate(label: string) {
        return labels[this._language][label] || labels._[label];
    }
}