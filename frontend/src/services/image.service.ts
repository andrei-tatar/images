import { HttpService } from './http.service';
import { Service } from '../util';
import * as _ from 'lodash';

@Service('imageService')
export class ImageService {
    private static readonly key = 'images:SortBy';
    private _sortBy: string;
    private _handlers: (() => void)[] = [];

    get sortBy() {
        return this._sortBy;
    }
    set sortBy(value: string) {
        if ((value === 'location' || value === 'date') && value !== this._sortBy) {
            localStorage.setItem(ImageService.key, value);
            this._sortBy = value;
            this._handlers.forEach(callback => callback());
        }
    }

    static $inject = ['httpService'];

    constructor(
        private httpService: HttpService,
    ) {
        this._sortBy = localStorage.getItem(ImageService.key) || 'date';
    }

    onRefresh(callback: () => void): () => void {
        this._handlers.push(callback);
        return () => _.pull(this._handlers, callback);
    }

    uploadImage(image, request) {
        return this.httpService.upload({
            url: 'UploadImage',
            data: {
                image,
                request: this.httpService.toJson(request),
            },
        });
    }

    addComment(imageId: string, comment: string) {
        return this.httpService.post({
            url: 'AddComment',
            data: {
                imageId,
                comment,
            },
        })
    }

    rateImage(imageId: string, rate: number) {
        return this.httpService.post({
            url: 'RateImage',
            data: {
                imageId,
                rate,
            },
        })
    }

    listImages(page: number, pageSize: number) {
        return this.httpService.get<{ images: any[] }>({
            url: 'ListImages',
            params: {
                page,
                pageSize,
                sortBy: this._sortBy,
            }
        }).then(r => r.data.images);
    }

    listImageComments(imageId: string) {
        return this.httpService.get({
            url: 'ListImageComments',
            params: {
                imageId,
            }
        }).then(r => r.data.comments);
    }

    getImageAverageRating(imageId: string) {
        return this.httpService.get({
            url: 'GetImageAverageRating',
            params: {
                imageId,
            },
        }).then(r => r.data);
    }
}
