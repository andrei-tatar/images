import { ScrollTopController } from './../../directives/scroll-top';
import { ImageService } from './../../services/image.service';
import component = require('./../home/home.component')
import { TranslationService } from './../../services/translations.service';
import { Component, Route } from '../../util';
import * as _ from 'lodash';

@Component('imageList', {
    template: require('./image-list.template.html'),
    require: {
        'scrollTop': '^',
    }
})
@Route({
    name: 'home.imagelist',
    url: '/',
    views: {
        'toolbar': {
            component: 'imageListToolbar',
        }
    }
})
class ImageListController implements ng.IOnInit, ng.IOnDestroy {
    static $inject = ['imageService'];
    private _isLoading = false;
    private _page = 0;
    private readonly _pageSize = 2;
    private _unsubscribe: () => void;

    hasMore = true;
    scrollTop: ScrollTopController;
    images: any[] = [];
    get noImages() {
        return !this.hasMore && this.images.length === 0;
    }

    constructor(
        private imageService: ImageService,
    ) {
    }

    $onInit(): void {
        this._unsubscribe = this.imageService.onRefresh(() => this.reloadImages());
    }

    $onDestroy(): void {
        this._unsubscribe();
    }

    private reloadImages() {
        this.images = [];
        this.hasMore = true;
        this._page = 0;
        this.scrollTop.scrollTop();
        this.loadMore();
    }

    async addComment(image, comment: string) {
        await this.imageService.addComment(image.id, comment);
        image.comments = await this.imageService.listImageComments(image.id);
    }

    async loadMore() {
        if (this._isLoading || !this.hasMore) return;
        try {
            this._isLoading = true;
            const images = this.images;

            var loaded = await this.imageService.listImages(this._page, this._pageSize);
            if (images !== this.images) {
                return;
            }

            this._page++;
            if (loaded.length < this._pageSize) {
                this.hasMore = false;
            }
            this.images.push(...loaded);
        } finally {
            this._isLoading = false;
        }
    }

    async rateImage(image, rate: number) {
        image.editRating = rate;
        image.userRating = rate;
        await this.imageService.rateImage(image.id, rate);
        image.averageRating = await this.imageService.getImageAverageRating(image.id);
    }
} 