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