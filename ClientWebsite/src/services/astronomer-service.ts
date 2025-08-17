import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Astronomer } from '../types/Astronomer';

@Injectable({
  providedIn: 'root'
})
export class AstronomerService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAstronomers() {
    return this.http.get<Astronomer[]>(this.baseUrl + "astronomer");
  }

  getAstronomer(id: number) {
    return this.http.get<Astronomer>(this.baseUrl + "astronomer/" + id);
  }

  updateAstronomer(astronomer: Astronomer) {
      console.log("UPDATING ASTRONOMER");
      console.log(astronomer);
    }

    addNewAstronomer(astronomer: Astronomer) {
      console.log("ADDING ASTRONOMER");
      console.log(astronomer);
    }

    removeAstronomer(astronomerId: Astronomer) {
      console.log("REMOVING ASTRONOMER");
    }
}
