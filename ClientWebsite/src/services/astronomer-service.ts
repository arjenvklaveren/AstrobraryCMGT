import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Astronomer } from '../types/Astronomer';
import { AstronomerFilterParams } from '../types/FilterParams';
import { SpaceBody } from '../types/SpaceBody';

@Injectable({
  providedIn: 'root'
})
export class AstronomerService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAstronomers(filterParams: AstronomerFilterParams | null) {

    let params = new HttpParams();
    if(filterParams != null) {
       if(filterParams.name != null) params = params.append('name', filterParams.name); 
        if(filterParams.age != null) params = params.append('age', filterParams.age); 
        if(filterParams.isMarried != null) params = params.append('isMarried', filterParams.isMarried); 
        if(filterParams.occupation != null) params = params.append('occupation', filterParams.occupation); 
    }

    return this.http.get<Astronomer[]>(this.baseUrl + "astronomer", {params});
  }

  getAstronomer(id: number) {
    return this.http.get<Astronomer>(this.baseUrl + "astronomer/" + id);
  }

  updateAstronomer(astronomer: Astronomer) {
    return this.http.put<Astronomer>(this.baseUrl + "astronomer/update", astronomer);
  }

  addNewAstronomer(astronomer: Astronomer) {
    delete astronomer.id;
    return this.http.post<number>(this.baseUrl + "astronomer/add", astronomer);
  }

  removeAstronomer(astronomerId: number) {
    return this.http.delete<Astronomer>(this.baseUrl + "astronomer/" + astronomerId + "/delete");
  }

  getDiscoveries(astronomerId: number) {
    return this.http.get<SpaceBody[]>(this.baseUrl + "astronomer/" + astronomerId + "/discoveries");
  }

  setAstronomerImage(astronomerId: number, file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(this.baseUrl + "astronomer/" + astronomerId + "/set-image",formData,{ responseType: 'text' });
  }
}
