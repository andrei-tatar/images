import { AuthService } from './../../services/auth.service';
import { Component, Route } from '../../util';

@Component('login', {
    template: require('./login.template.html')
})
@Route({
    name: 'login',
    url: '/login',
})
class LoginController implements ng.IOnInit, ng.IOnDestroy {

    static $inject = ['authService', '$state'];

    constructor(
        private authService: AuthService,
        private $state: ng.ui.IStateService,
    ) {

    }

    $onInit(): void {
        (window as any).checkLoginState = () => this.checkLoginState();
    }

    $onDestroy(): void {
        delete (window as any).checkLoginState;
    }

    async checkLoginState() {
        const isLoggedIn = await this.authService.isLoggedIn();
        if (isLoggedIn) this.$state.go('home.imagelist');
    }
}