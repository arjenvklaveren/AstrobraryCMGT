import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { SpaceBody } from '../types/SpaceBody';
import { SpaceBodyFilterParams } from '../types/FilterParams';

@Injectable({
  providedIn: 'root'
})
export class SpacebodyService {
    http = inject(HttpClient);
    baseUrl = environment.apiUrl;
  
    getBodies(filterParams : SpaceBodyFilterParams) {

      let params = new HttpParams();
      if(filterParams.name) params = params.append('name', filterParams.name); 
      if(filterParams.age) params = params.append('age', filterParams.age); 
      if(filterParams.hasRings) params = params.append('hasRings', filterParams.hasRings); 
      if(filterParams.bodyType) params = params.append('bodyType', filterParams.bodyType); 

      return this.http.get<SpaceBody[]>(this.baseUrl + "spacebody", {params});
    }

    getBody(id: number) {
      return this.http.get<SpaceBody>(this.baseUrl + "spacebody/" + id);
    }

    getBodyHierarchy(id: number, full: boolean = true){
      return this.http.get<SpaceBody>(this.baseUrl + "spacebody/" + id + "/hierarchy");
    }
}
