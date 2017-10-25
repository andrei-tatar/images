import { Component, Route } from '../../util';

@Component('login', {
    template: require('./login.template.html')
})
@Route({
    name: 'login',
    url: '/login',
})
class LoginController {
    get test() {
        return 'It works!'
    }
}