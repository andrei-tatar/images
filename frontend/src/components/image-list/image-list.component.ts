import { ImageService } from './../../services/image.service';
import { TranslationService } from './../../services/translations.service';
import { Component, Route } from '../../util';
import * as _ from 'lodash';

@Component('imageList', {
    template: require('./image-list.template.html'),
})
@Route({
    name: 'home.imagelist',
    url: '/',
})
class ImageListController implements ng.IOnInit {

    static $inject = ['imageService'];

    images: any[];

    constructor(
        private imageService: ImageService
    ) {
    }

    async $onInit() {
        this.images = await this.imageService.listImages(0, 10);
    }

    async addComment(image, comment: string) {
        await this.imageService.addComment(image.id, comment);
        image.comments = await this.imageService.listImageComments(image.id);
    }
} 