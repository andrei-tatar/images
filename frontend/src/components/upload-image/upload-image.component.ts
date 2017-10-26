import { ToastService } from './../../services/toast.service';
import { TranslationService } from './../../services/translations.service';
import { ImageService } from './../../services/image.service';
import { handleValidationErrors } from "./../../util";
import { Component } from "./../../util";
import { Route } from "./../../util";

@Component('uploadImage', {
    template: require('./upload-image.template.html'),
})
@Route({
    name: 'home.add',
    url: '/upload',
})
class UploadImageController {
    uploadForm: ng.IFormController;

    imageTags: string[] = [];
    file;
    description: string;
    location: string;
    date: Date;

    static $inject = ['$state', 'imageService', 'toastService'];

    constructor(
        private $state: ng.ui.IStateService,
        private imageService: ImageService,
        private toastService: ToastService,
    ) {

    }

    cancel() {
        this.$state.go('home.imagelist');
    }

    upload() {
        this.imageService.uploadImage(this.file, {
            tags: this.imageTags,
            description: this.description,
            date: this.date,
            location: this.location,
        }).catch(err => {
            if (!handleValidationErrors(err, this.uploadForm))
                this.toastService.show('upload-image.error');
            throw err;
        }).then(() => {
            this.cancel();
            this.toastService.show('upload-image.success');
        });
    }
}