import { IRouteConfig } from './../../util';
import { AuthService } from './../../services/auth.service';
import { Run } from '../../util';

Run(($transitions, auth: AuthService) => {
    $transitions.onBefore({}, async transition => {
        const routeSettings: IRouteConfig = transition.$to().self;
        if (routeSettings.requiresLogin === void 0 || routeSettings.requiresLogin) {
            const isAuth = await auth.isLoggedIn();
            if (!isAuth) {
                return transition.router.stateService.target('home.login');
            }
        }
        return true;
    })
}, '$transitions', 'authService');