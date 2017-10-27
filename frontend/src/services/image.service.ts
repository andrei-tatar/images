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
            url: `UploadImage`,
            data: {
                image,
                request: this.httpService.toJson(request),
            },
        });
    }

    listImages(page: number, pageSize: number) {
        return this.httpService.get<{ images: { id: string, link: string }[] }>({
            url: 'ListImages',
            params: {
                page,
                pageSize,
            }
        }).then(r => r.data.images);
    }
}