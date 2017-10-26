import { Service } from "../util";

@Service('imageService')
export class ImageService {
    static $inject = ['$http', 'Upload', 'config'];

    constructor(
        private $http: ng.IHttpService,
        private upload: ng.angularFileUpload.IUploadService,
        private config: IConfig,
    ) {
    }

    uploadImage(image, request) {
        return this.upload.upload({
            method: 'POST',
            url: `${this.config.apiEndpoint}Command/UploadImage`,
            data: {
                image,
                request: this.upload.json(request),
            },
        });
    }
}