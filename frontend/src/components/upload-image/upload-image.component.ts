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

}