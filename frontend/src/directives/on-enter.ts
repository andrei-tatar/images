import { Directive } from '../util';

Directive('onEnter', () => ({
    restrict: 'A',
    scope: {
        action: '&onEnter'
    },
    link: function (scope, element, attrs) {
        element.on('keydown keypress', function (event) {
            if (event.which === 13) {
                scope.$apply((scope as any).action);
                event.preventDefault();
            }
        });
    }
}));