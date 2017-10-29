import { AuthService } from './auth.service';
import { Service, Filter } from './../util';

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

    upload<T = any>(options: IRequest) {
        this.extendHeaders(options);
        options.method = 'POST';
        options.url = this.config.apiEndpoint + 'Command/' + options.url;
        return this._upload.upload<T>(options as ng.angularFileUpload.IFileUploadConfigFile);
    }

    get<T = any>(options: IRequest) {
        this.extendHeaders(options);
        options.method = 'GET';
        options.url = this.config.apiEndpoint + 'Query/' + options.url;
        return this.$http<T>(options as ng.IRequestConfig);
    }

    post<T = any>(options: IRequest) {
        this.extendHeaders(options);
        options.method = 'POST';
        options.url = this.config.apiEndpoint + 'Command/' + options.url;
        return this.$http<T>(options as ng.IRequestConfig);
    }

    toJson(data: any): string {
        return this._upload.json(data);
    }

    extendHeaders(request: IRequest) {
        if (!request.headers) request.headers = {};
        request.headers['Authorization'] = `Bearer ${this.authService.token}`;
    }
}

Filter('apiEndpoint', apiEndpointFilter, 'config')
function apiEndpointFilter(config: IConfig) {
    return (value) => {
        return config.apiEndpoint + value;
    }
}

interface IRequest {
    url: string;
    headers?: ng.IHttpRequestConfigHeaders;
    data?: any,
    [key: string]: any;
}