import { Injectable, Inject } from '@angular/core';
import { HttpInterceptor, HttpResponse } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { NzMessageService } from 'ng-zorro-antd/message';

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {
    constructor(private message: NzMessageService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    var responseBody = event.body as ApiResponse;
                    if (responseBody.statusCode != 200) {
                        if (responseBody.errorCode == 1)
                            throw new Error(responseBody.details);
                        else
                        {
                            console.log(responseBody.details);
                            this.message.info(responseBody.details);
                        }
                    }
                    return event.clone({ body: responseBody.data });
                }
            })
        );
    }
}
