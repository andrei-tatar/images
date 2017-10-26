import { TranslationService } from './../../services/translations.service';
import { Component, Route } from '../../util';

@Component('imageList', {
    template: require('./image-list.template.html'),
})
@Route({
    name: 'home.imagelist',
    url: '/',
})
class ImageListController {
} 