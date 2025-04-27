import type { Property } from './Property.ts';

export interface Item {
  id: string;
  points: number;
  description: string;
  image: string;
  name: string;
  propertyValues: PropertyValue[];
}

export interface PropertyValue {
  value: string;
  property: Property;
}
