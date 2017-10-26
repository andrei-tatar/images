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
    imageTags: string[] = [];
    file;
    description: string;
    location: string;
    date: Date;
    uploadForm: ng.IFormController;

    static $inject = ['$state'];

    constructor(
        private $state: ng.ui.IStateService,
    ) {

    }

    cancel() {
        this.$state.go('home.imagelist');
    }

    upload() {

    }
}