import { ImageService } from './../../services/image.service';
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

    static $inject = ['$state', 'imageService'];

    constructor(
        private $state: ng.ui.IStateService,
        private imageService: ImageService,
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
        });
    }
}