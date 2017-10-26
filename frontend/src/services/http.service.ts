import { AuthService } from './auth.service';
import { Service } from "./../util";

@Service('httpService')
export class HttpService {
    static $inject = ['$http', 'Upload', 'config', 'authService'];

    constructor(
        private $http: ng.IHttpService,
        private _upload: ng.angularFileUpload.IUploadService,
        private config: IConfig,
        private authService: AuthService,
    ) {

    }

    upload(options: ng.angularFileUpload.IFileUploadConfigFile) {
        this.extendHeaders(options);
        options.url = this.config.apiEndpoint + options.url;
        return this._upload.upload(options);
    }

    toJson(data: any): string {
        return this._upload.json(data);
    }

    extendHeaders(request: ng.IRequestConfig) {
        if (!request.headers) request.headers = {};
        request.headers['Authorization'] = `Bearer ${this.authService.token}`;
    }
}