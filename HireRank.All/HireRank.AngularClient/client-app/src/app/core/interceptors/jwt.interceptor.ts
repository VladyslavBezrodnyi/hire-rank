import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorizationService } from 'src/app/core/services/authorization.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthorizationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(this.authenticationService.currentUser != null) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ` + this.authenticationService.currentUser.token
                }
            });
        }
        return next.handle(request);
    }
}
