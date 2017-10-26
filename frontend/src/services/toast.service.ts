import { TranslationService } from './translations.service';
import { Service } from "./../util";

@Service('toastService')
export class ToastService {
    static $inject = ['$mdToast', 'translationService'];

    constructor(
        private $mdToast: ng.material.IToastService,
        private translationService: TranslationService,
    ) {
    }

    show(msg: string) {
        const translated = this.translationService.translate(msg);
        return this.$mdToast.show(this.$mdToast.simple().textContent(translated));
    }
}