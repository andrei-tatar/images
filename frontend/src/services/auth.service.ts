
import { Service, Filter } from "./../util";
import * as _ from "lodash";

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

Filter('getProfileImage', getProfileImageFilter)
function getProfileImageFilter() {
    return (userId) => `//graph.facebook.com/v2.10/${userId}/picture`;
}

Filter('getUserName', getUserNameFilter, '$q')
function getUserNameFilter($q: ng.IQService) {
    const map: { [key: string]: string } = {};
    const resolve = _.memoize(async (userId) => {
        const defered = $q.defer<string>();
        FB.api(`/${userId}`, function (response) {
            defered.resolve(response.name);
        });
        map[userId] = await defered.promise;
    });

    const filter = (userId) => {
        resolve(userId);
        return map[userId];
    };

    (filter as any).$stateful = true;
    return filter;
}