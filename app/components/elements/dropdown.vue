<template>
  <div class="mb3 col-12" :class="`sm-col-${size}`">
    <div class="relative">
      <input type="text" :value="displayValue" readonly
        class="m0 py15 border-none input-line transition h5 w100"
        :class="{ error: errors && errors.length }"
        :id="`fld${_uid}`" @focus="focus = true" @blur="onBlur">
      <template v-if="!focus">
        <label class="py15 absolute top-0 transition h5 nudge gray noclick"
          :class="{ active: value }" :for="`fld${_uid}`">
          {{ label }}
        </label>
        <span class="absolute top-0 right-0 py15 noclick gray">▼</span>
      </template>
      <Outside v-if="focus" class="absolute top-0 bg-white shadow1 w100 z2"
        @click="focus = false">
        <div v-for="opt in options" :key="opt[idField]" @click="pick(opt)"
          class="p2 pointer h-bg-silver" :class="{ 'bg-silver': choice == opt }">
          {{ opt[labelField] }}
        </div>
      </Outside>
      <label v-if="hint" class="block gray h7">{{ hint }}</label>
      <template v-if="errors">
        <label v-for="error in errors" :key="error" class="block red h7">
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
      errors: {
        type: Array,
        required: false,
        default: null,
      },
    },
    data() {
      return {
        focus: false,
      };
    },
    computed: {
      choice() {
        return this.options.find(a => a[this.idField] == this.value);
      },
      displayValue() {
        return this.choice && this.choice[this.labelField];
      },
    },
    methods: {
      pick(option) {
        this.$emit("input", option[this.idField]);
        this.focus = false;
      },
      onBlur() {
        setTimeout(() => this.focus = false, 50);
      }
    },
  };
</script>
