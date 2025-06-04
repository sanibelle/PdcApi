export interface Property {
    id: string
    name: string
    type: string
    value: string
}

export interface BooleanProperty extends Property {
    isChecked: boolean
    type: 'boolean'
}

export interface NumericProperty extends Property {
    type: 'numeric'
    min: number
    max: number
}

export interface OptionProperty extends Property {
    type: 'option'
    options: Option[]
    selectedOption: string
}

export interface Option {
    id: string
    value: string
}