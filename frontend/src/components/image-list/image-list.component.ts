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
class ImageListController {

    static $inject = ['imageService'];
    private _isLoading = false;
    private _page = 0;
    private readonly _pageSize = 2;

    hasMore = true;
    images: any[] = [];
    get noImages() {
        return !this.hasMore && this.images.length === 0;
    }

    constructor(
        private imageService: ImageService
    ) {
    }

    async addComment(image, comment: string) {
        await this.imageService.addComment(image.id, comment);
        image.comments = await this.imageService.listImageComments(image.id);
    }

    async loadMore() {
        if (this._isLoading || !this.hasMore) return;
        this._isLoading = true;

        var loaded = await this.imageService.listImages(this._page, this._pageSize);
        this._page++;
        if (loaded.length < this._pageSize) {
            this.hasMore = false;
        }
        this.images.push(...loaded);
        this._isLoading = false;
    }

    async rateImage(image, rate: number) {
        image.editRating = rate;
        image.userRating = rate;
        await this.imageService.rateImage(image.id, rate);
    }
} 