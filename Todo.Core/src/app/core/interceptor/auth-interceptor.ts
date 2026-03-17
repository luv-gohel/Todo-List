import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const userGuid = localStorage.getItem('userGuid');
  if(userGuid){
    const cloned = req.clone({
      setHeaders:{
        'X-User-Guid':userGuid
      }
    });
    return next(cloned);
  }
  return next(req);
};
