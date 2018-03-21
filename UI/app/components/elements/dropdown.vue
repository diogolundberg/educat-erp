<template>
  <div :class="`mb3 col-12 sm-col-${size}`">
    <div class="relative">
      <input
        :value="displayValue"
        :disabled="isDisabled"
        :class="{
          'active': focus,
          'error': errors && errors.length,
          'dots-bottom': isDisabled,
          'input-line': !isDisabled,
        }"
        :id="`fld${_uid}`"
        type="text"
        readonly
        class="m0 py2 border-none ease h5 w100 bg-transparent"
        @focus="focus = true"
        @click="focus = true"
        @blur="onBlur">
      <label
        v-if="!focus"
        :class="{ nudge: focus || value }"
        :for="`fld${_uid}`"
        class="py2 absolute top-0 left-0 bounce h5 dim noclick">
        {{ label }}
      </label>
      <span
        v-if="!focus"
        class="absolute top-0 right-0 py2 noclick dim">
        ▼
      </span>
      <Outside
        v-if="focus"
        class="absolute top-0 bg-white shadow1 w100 z2 overflow-scroll height-1"
        @click="focus = false">
        <div
          v-for="opt in options"
          :key="opt[idField]"
          :class="{ 'bg-silver': choice === opt }"
          class="p2 pointer h-bg-silver"
          @click="pick(opt)">
          {{ opt[labelField] }}
        </div>
      </Outside>
      <label
        v-if="hint"
        class="block gray h7">{{ hint }}</label>
      <template v-if="allErrors">
        <label
          v-for="error in allErrors"
          :key="error"
          class="block red h7">
          {{ error }}
        </label>
      </template>
    </div>
  </div>
</template>

<script>
  export default {
    name: "DropDown",
    props: {
      value: {
        type: [String, Number],
        required: false,
        default: null,
      },
      label: {
        type: String,
        required: true,
      },
      options: {
        type: Array,
        required: true,
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      hint: {
        type: String,
        required: false,
        default: null,
      },
      size: {
        type: Number,
        required: false,
        default: 12,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      errors: {
        type: Array,
        required: false,
        default: () => [],
      },
      required: {
        type: Boolean,
        default: false,
      },
    },
    data() {
      return {
        focus: false,
        validate: false,
      };
    },
    computed: {
      choice() {
        return this.options.find(a => a[this.idField] === this.value);
      },
      displayValue() {
        return this.choice && this.choice[this.labelField];
      },
      isDisabled() {
        return this.disabled || !this.options.length;
      },
      allErrors() {
        return [].concat(this.errors || [], this.localErrors);
      },
      localErrors() {
        return [
          this.required && !this.value && "Campo obrigatório",
        ].filter(a => a && this.validate);
      },
    },
    methods: {
      pick(option) {
        this.focus = false;
        this.$emit("input", option[this.idField]);
      },
      onBlur() {
        this.validate = true;
        setTimeout(() => {
          this.focus = false;
        }, 150);
      },
    },
  };
</script>
