import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { CarsModule } from './cars/cars.module'
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './core/header/header.component';
import { NavigationComponent } from './core/navigation/navigation.component';
import { SharedModule } from './shared/shared.module';
import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthenticationModule } from './core/authentication/authentication.module';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';




@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NavigationComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AuthenticationModule,
    SharedModule,
    CarsModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    NgbModule,
    
  ],

  providers: [{
    provide: HTTP_INTERCEPTORS, 
    useClass: AuthInterceptor, 
    multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
