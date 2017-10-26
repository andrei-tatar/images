import { TranslationService } from './../services/translations.service';
import { Filter } from '../util';

Filter('translate', TranslateFilter, 'translationService')
function TranslateFilter(translationService: TranslationService) {
    function translate(value, ...args: any[]) {
        return translationService.translate(value) || value;
    }

    (translate as any).$stateful = true;
    return translate;
}