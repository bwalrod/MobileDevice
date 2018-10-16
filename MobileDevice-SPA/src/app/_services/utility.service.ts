import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  objectsEqual(obj1: any, obj2: any) {
    return JSON.stringify(obj1).toLowerCase() === JSON.stringify(obj2).toLowerCase();
  }

}
