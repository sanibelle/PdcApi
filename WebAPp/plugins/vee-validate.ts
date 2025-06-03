import { defineRule, configure } from 'vee-validate';
import {
  required,
  email,
  min,
  max,
  regex,
  max_value,
  min_value,
  numeric,
  integer,
} from '@vee-validate/rules';
import { localize, setLocale } from '@vee-validate/i18n';

export default defineNuxtPlugin(() => {
  // language
  setLocale('fr');
  // rules
  defineRule('required', required);
  defineRule('email', email);
  defineRule('min', min);
  defineRule('max', max);
  defineRule('regex', regex);
  defineRule('max_value', max_value);
  defineRule('min_value', min_value);
  defineRule('numeric', numeric);
  defineRule('integer', integer);

  // Exemple de custom rule
  defineRule('date', (value: any) => {
    if (!value) {
      return true;
    }
    if (!isDate(value)) {
      return false;
    }
    return true;
  });

  // Configure VeeValidate globally
  configure({
    generateMessage: localize({
      fr: {
        messages: {
          required: 'Ce champ est requis',
          email: 'Veuillez saisir une adresse e-mail valide',
          min: ({ rule }) =>
            rule?.params
              ? `Ce champ doit contenir au moins ${rule?.params} caractères`
              : 'Ce champ est trop court',
          max: ({ rule }) =>
            rule?.params
              ? `Ce champ ne doit pas dépasser ${rule?.params} caractères`
              : 'Ce champ est trop long',
          regex: 'Le format de ce champ est invalide',
          max_value: ({ rule }) =>
            rule?.params
              ? `La valeur ne doit pas dépasser ${rule?.params}`
              : 'Cette valeur est trop élevée',
          min_value: ({ rule }) =>
            rule?.params
              ? `La valeur doit être au moins ${rule?.params}`
              : 'Cette valeur est trop basse',
          numeric: 'Ce champ doit être un nombre',
          integer: 'Ce champ doit être un nombre entier',
          date: 'La date saisie est invalide',
        },
      },
    }),
  });
});
