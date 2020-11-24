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
                    if (responseBody.StatusCode != 200 && responseBody.StatusCode != 201 && responseBody.StatusCode != 203 && responseBody.StatusCode != 204) {
                        if (responseBody.ErrorCode == 1)
                            throw new Error(responseBody.Details);
                        else
                        {
                            console.log(responseBody.Details);
                            this.message.info(responseBody.Details);
                        }
                    }
                    return event.clone({ body: responseBody.Data });
                }
            })
        );
    }
}
