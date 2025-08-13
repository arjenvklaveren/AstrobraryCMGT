import { Routes } from '@angular/router';
import { SpaceBodyList } from '../features/space-body-list/space-body-list';
import { HomePage } from '../features/home-page/home-page';
import { AstronomersList } from '../features/astronomers-list/astronomers-list';
import { ArAppPage } from '../features/ar-app-page/ar-app-page';
import { ApiPage } from '../features/api-page/api-page';
import { AboutPage } from '../features/about-page/about-page';
import { SpaceBodyInfo } from '../features/space-body/space-body-info';

export const routes: Routes = [
    { path: '', component: HomePage },
    { path: 'bodies', component: SpaceBodyList },
    { path: 'bodies/:id', component: SpaceBodyInfo },
    { path: 'astronomers', component: AstronomersList },
    { path: 'api', component: ApiPage },
    { path: 'application', component: ArAppPage },
    { path: 'about', component: AboutPage }
];
