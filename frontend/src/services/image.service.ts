import { HttpService } from './http.service';
import { Service } from "../util";

@Service('imageService')
export class ImageService {
    private static readonly key = 'images:SortBy';
    private _sortBy: string;

    get sortBy() {
        return this._sortBy;
    }
    set sortBy(value: string) {
        if (value === 'location' || value === 'date') {
            localStorage.setItem(ImageService.key, value);
            this._sortBy = value;
        }
    }

    constructor(
        private httpService: HttpService,
    ) {
        this.sortBy = localStorage.getItem(ImageService.key) || 'date';
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
