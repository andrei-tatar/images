import { HttpService } from './http.service';
import { Service } from "../util";

@Service('imageService')
export class ImageService {

    constructor(
        private httpService: HttpService,
    ) {
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
}
