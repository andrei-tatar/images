import { TranslationService } from './../services/translations.service';
import { module } from './../util';


module.filter('translate', TranslateFilter)

TranslateFilter.$inject = ['translationService'];
function TranslateFilter(translationService: TranslationService) {
    function translate(value, ...args: any[]) {
        return translationService.translate(value) || value;
    }

    (translate as any).$stateful = true;
    return translate;
}