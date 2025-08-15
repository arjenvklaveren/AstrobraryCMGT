import 'reflect-metadata';

export function Field(options?: { label?: string, type?: any, exclude?: boolean }) {
    return function (target: any, propertyKey: string) {
        const inferredType = Reflect.getMetadata('design:type', target, propertyKey);
        const type = options?.type || inferredType;

        // Auto-mark enums as "enum" kind
        const isEnum =  (
            typeof type === 'object' &&
                type !== null &&
                Object.values(type).every(v => typeof v === 'string' || typeof v === 'number')
                ? true
                : false
        );
        const exclude = options?.exclude == undefined ? false : options?.exclude == true ? true : false;
        const keyNamePascal = propertyKey.charAt(0).toUpperCase() + propertyKey.slice(1);
        const fieldName = options?.label != null ? options?.label : keyNamePascal;

        const fields = Reflect.getMetadata('form:fields', target) || [];
        fields.push({ label: fieldName, key: propertyKey, type, isEnum, exclude });
        Reflect.defineMetadata('form:fields', fields, target);
    };
}

export class TypeDetect {
    StringType = String;
    NumberType = Number;
    BoolType = Boolean;
}