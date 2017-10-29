import { Directive } from '../util';
import * as _ from 'lodash';

Directive('scrollTop', () => ({
    restrict: 'A',
    controller: ScrollTopController,
    link: function (scope, element, attrs, controller: ScrollTopController) {
        const remove = controller.onScrollTop(() => {
            element[0].scrollTo({ top: 0 });
        });
        scope.$on('$destroy', () => {
            remove();
        });
    }
}));

export class ScrollTopController {
    private _callbacks: (() => void)[] = [];

    public onScrollTop(c: () => void): () => void {
        this._callbacks.push(c);
        return () => _.pull(this._callbacks, c);
    }

    public scrollTop() {
        this._callbacks.forEach(c => c());
    }
}