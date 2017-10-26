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

    static $inject = ['$state', 'imageService', '$mdToast'];

    constructor(
        private $state: ng.ui.IStateService,
        private imageService: ImageService,
        private $mdToast: ng.material.IToastService,
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
            if (handleValidationErrors(err, this.uploadForm))
                return;
            this.$mdToast.showSimple('Error: Something went wrong');
        }).then(() => {
            this.cancel();
            this.$mdToast.showSimple('Image uploaded');
        });
    }
}