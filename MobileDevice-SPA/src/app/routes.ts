import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        children: [
            { path: 'users', component: UserListComponent},
            { path: 'users/edit/:id', component: UserEditComponent, resolve: {user: UserEditResolver}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
