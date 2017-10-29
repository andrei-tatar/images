import * as angular from 'angular'

const module = angular.module('AzetsApp');

export function Component(name: string, value: ng.IComponentOptions) {
    return function (target) {
        value.controller = target;
        if (target.__componentNameResolved) {
            target.__componentNameResolved(name);
        } else {
            target.__componentName = name;
        }
        module.component(name, value);
    }
}

export function Route(route: IRouteConfig) {
    return function (target) {
        const resolved = (name) => {
            if (route.views) {
                route.views[''] = name;
            } else {
                route.component = name;
            }
            module.config(['$stateRegistryProvider', $stateRegistry => $stateRegistry.register(route)]);
        }
        if (target.__componentName) {
            resolved(target.__componentName);
        } else {
            target.__componentNameResolved = resolved;
        }
    }
}

export interface IRouteConfig extends ng.ui.IState {
    requiresLogin?: boolean;
}

export function Service(name: string) {
    return function (target) {
        module.service(name, target);
    }
}

export function Directive(name, factory: (...args: any[]) => ng.IDirective, ...inject: string[]) {
    module.directive(name, [...inject, factory]);
}

export function Run(run: (...args: any[]) => void, ...inject: string[]) {
    module.run([...inject, run]);
}

export function Filter(name, filter: Function, ...inject: string[]) {
    filter.$inject = inject;
    module.filter(name, filter);
}

export function handleValidationErrors(err: ng.IHttpResponse<{ validationErrors: { field: string, code: string }[] }>, form: ng.IFormController) {
    if (err.status === 400 && err.data.validationErrors) {
        for (const error of err.data.validationErrors) {
            const input = form[error.field] as ng.INgModelController;
            input.$error[error.code] = true;
            input.$setTouched();
            const listener = () => {
                delete input.$error[error.code];
                const index = input.$viewChangeListeners.indexOf(listener);
                input.$viewChangeListeners.splice(index, 1);
            };
            input.$viewChangeListeners.push(listener);
        }

        return true;
    }
    return false;
}