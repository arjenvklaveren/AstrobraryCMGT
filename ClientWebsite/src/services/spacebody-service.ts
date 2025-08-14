import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { SpaceBody } from '../types/SpaceBody';

@Injectable({
  providedIn: 'root'
})
export class SpacebodyService {
    http = inject(HttpClient);
    baseUrl = environment.apiUrl;
  
    getBodies() {
      return this.http.get<SpaceBody[]>(this.baseUrl + "spacebody");
    }

    getBody(id: number) {
      return this.http.get<SpaceBody>(this.baseUrl + "spacebody/" + id);
    }

    getBodyHierarchy(id: number, full: boolean = true){
      return this.http.get<SpaceBody>(this.baseUrl + "spacebody/" + id + "/hierarchy");
    }
}
