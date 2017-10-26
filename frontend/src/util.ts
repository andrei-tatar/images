import * as angular from 'angular'

export const module = angular.module('AzetsApp');

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

export function Route(route: ng.ui.IState) {
    return function (target) {
        const resolved = (name) => {
            route.component = name;
            module.config(['$stateRegistryProvider', $stateRegistry => $stateRegistry.register(route)]);
        }
        if (target.__componentName) {
            resolved(target.__componentName);
        } else {
            target.__componentNameResolved = resolved;
        }
    }
}

export function Service(name: string) {
    return function (target) {
        module.service(name, target);
    }
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