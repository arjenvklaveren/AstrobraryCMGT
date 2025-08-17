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
  
    getBodies(filterParams : SpaceBodyFilterParams | null) {

      let params = new HttpParams();
      if(filterParams != null) {
        if(filterParams.name != null) params = params.append('name', filterParams.name); 
        if(filterParams.age != null) params = params.append('age', filterParams.age); 
        if(filterParams.hasRings != null) params = params.append('hasRings', filterParams.hasRings); 
        if(filterParams.bodyType != null) params = params.append('bodyType', filterParams.bodyType); 
      }
     
      return this.http.get<SpaceBody[]>(this.baseUrl + "spacebody", {params});
    }

    getBody(id: number) {
      return this.http.get<SpaceBody>(this.baseUrl + "spacebody/" + id);
    }

    getBodyHierarchy(id: number, full: boolean = true) {
      return this.http.get<SpaceBody>(this.baseUrl + "spacebody/" + id + "/hierarchy");
    }

    updateBody(spaceBody: SpaceBody) {
      console.log("UPDATING BODY");
      console.log(spaceBody);
    }

    addNewBody(spaceBody: SpaceBody) {
      console.log("ADDING BODY");
      console.log(spaceBody);
    }

    removeBody(spaceBodyId: number) {
      console.log("REMOVING BODY");
    }
}
