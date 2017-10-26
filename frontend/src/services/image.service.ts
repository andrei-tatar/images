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
            method: 'POST',
            url: `Command/UploadImage`,
            data: {
                image,
                request: this.httpService.toJson(request),
            },
        });
    }
}