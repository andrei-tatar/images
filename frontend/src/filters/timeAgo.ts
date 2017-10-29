import { Filter } from '../util';
import * as moment from 'moment';

Filter('timeAgo', timeAgoFilter)
function timeAgoFilter() {
    const filter = (time) => moment(time).fromNow();
    (filter as any).$stateful = true;
    return filter;
}