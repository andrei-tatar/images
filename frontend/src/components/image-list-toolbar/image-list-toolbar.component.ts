import { ImageService } from './../../services/image.service';
import { Component } from './../../util';

@Component('imageListToolbar', {
    template: require('./image-list-toolbar.html')
})
class ImageListToolbarComponent {
    static $inject = ['imageService'];

    get filter() {
        return this.imageService.filter;
    }
    set filter(value: string) {
        this.imageService.filter = value;
    }

    get sortBy() {
        return this.imageService.sortBy;
    }
    set sortBy(value) {
        this.imageService.sortBy = value;
    }

    constructor(
        private imageService: ImageService,
    ) {

    }
}