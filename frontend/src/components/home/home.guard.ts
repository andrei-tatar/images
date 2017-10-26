import { AuthService } from './../../services/auth.service';
import { Run } from "../../util";

Run(($transitions, auth: AuthService) => {
    $transitions.onBefore({ to: 'home.*' }, async transition => {
        const isAuth = await auth.isLoggedIn();
        if (isAuth) return true;
        return transition.router.stateService.target('login');
    })
}, '$transitions', 'authService');