import { AuthService } from './../../services/auth.service';
import { Component, Route } from '../../util';

@Component('login', {
    template: require('./login.template.html')
})
@Route({
    name: 'home.login',
    url: '/login',
    requiresLogin: false,
})
class LoginController implements ng.IOnInit {

    static $inject = ['authService', '$state'];

    constructor(
        private authService: AuthService,
        private $state: ng.ui.IStateService,
    ) {

    }

    $onInit(): void {
        this.checkLoginState();
    }

    async login() {
        FB.login(response => {
            if (!this.checkLoginState()) {
                console.log('User cancelled login or did not fully authorize.');
            }
        });
    }

    private async checkLoginState() {
        const isLoggedIn = await this.authService.isLoggedIn();
        if (isLoggedIn) this.$state.go('home.imagelist');
        return isLoggedIn;
    }
}