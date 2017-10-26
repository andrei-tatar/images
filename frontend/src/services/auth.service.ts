
import { Service } from "./../util";

@Service('authService')
export class AuthService {

    static $inject = ['$q', '$state']

    private _token: string;

    constructor(
        private $q: ng.IQService,
        private $state: ng.ui.IStateService,
    ) {
        FB.init({
            appId: '338676759931156',
            cookie: true,
            xfbml: true,
            version: 'v2.8'
        });
    }

    get token() {
        return this._token;
    }

    isLoggedIn() {
        const defered = this.$q.defer<boolean>();

        FB.getLoginStatus(res => {
            if (res.status === 'connected') {
                this._token = res.authResponse.accessToken;
                defered.resolve(true);
            }
            else {
                defered.resolve(false);
            }
            console.log(res);
        });

        return defered.promise;
    }

    logout() {
        FB.logout(() => { 
            this.$state.go('login');
        });
    }

    getUserInfo() {
        const defered = this.$q.defer();
        FB.api('/me', (res) => {
            debugger;
            defered.resolve(res);
        });
        return defered.promise;
    }
}

